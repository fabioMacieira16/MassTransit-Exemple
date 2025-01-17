﻿using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Model;
using System;
using System.Threading.Tasks;

namespace OrderConsumer.Controller
{
    public class OrderController : ControllerBase
    {
        private readonly IBus _bus;

        public OrderController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> creatTicket(Ticket tickt)
        {
            if (tickt != null)
            {
                tickt.Booked = DateTime.Now;
                Uri uri = new Uri("rabbitmq://localhost/orderTicketQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(tickt);

                return Ok();
            }
            return BadRequest();
        }
    }
}
