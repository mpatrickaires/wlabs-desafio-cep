﻿using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using WLabsDesafioCEP.Application.Exceptions;
using WLabsDesafioCEP.WebAPI.Common.Dtos;

namespace WLabsDesafioCEP.WebAPI.Common.Extensions
{
    public static class WebApplicationExtensions
    {
        private const string MensagemGenerica = "Ocorreu um erro durante o processamento da requisição!";
        private const string ContentType = "application/json";

        public static void ConfigurarMiddlewareTratamentoException(this WebApplication app)
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    Exception? exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                    if (exception == null) 
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = ContentType;
                        await context.Response.WriteAsJsonAsync(new RespostaApiDto { Mensagem = MensagemGenerica });
                        return;
                    }

                    context.Response.StatusCode = (int)ObterStatusCodePelaException(exception);
                    context.Response.ContentType = ContentType;
                    
                    if (context.Response.StatusCode == (int)HttpStatusCode.InternalServerError)
                    {
                        await context.Response.WriteAsJsonAsync(new RespostaApiDto { Mensagem = MensagemGenerica });
                    }
                    else
                    {
                        await context.Response.WriteAsJsonAsync(new RespostaApiDto { Mensagem = exception.Message });
                    }
                });
            });
        }

        private static HttpStatusCode ObterStatusCodePelaException(Exception exception) => exception switch
        {
            ValidacaoException => HttpStatusCode.BadRequest,
            NaoEncontradoException => HttpStatusCode.NotFound,
            _ => HttpStatusCode.InternalServerError,
        };
    }
}
