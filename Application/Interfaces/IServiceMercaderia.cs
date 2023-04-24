﻿using Application.Schemas;
using Application.UseCase;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IServiceMercaderia
    {
        //1
        public Task<bool> Exists(string name);
        public Task<MercaderiaResponse> Create(MercaderiaRequest body);
        //4
        public Task<List<MercaderiaGetResponse>>? GetFiltered(int tipo, string? nombre, string orden = "ASC");
        //5
        public Task<bool> Exists(int id);
        public Task<MercaderiaResponse> Update(int id, MercaderiaRequest body);
        //6
        public Task Delete(int id);
        //7
        public Task<MercaderiaResponse?> GetMercaderiaById(int id);

    }
}
