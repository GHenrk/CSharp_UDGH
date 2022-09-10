﻿using CSharpUdemy_MVC.Data;
using CSharpUdemy_MVC.Models;
using CSharpUdemy_MVC.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CSharpUdemy_MVC.Services
{
    public class SellerService
    {

        private readonly CSharpUdemy_MVCContext _context;

        public SellerService(CSharpUdemy_MVCContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)

        {   //Adiciona o primeiro departamento ao objeto, para teste de cadastro!
            //obj.Department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            //busca no contexto o Seller, primeiro ou padrao cujo obj.Id seja igual ao Id enviado no parametro;
            //Retorna o OBJ;
            //Funcao include para carregar também o odepartment do obj;
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Elemento não encontrado");
            }
            try 
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }



    }
}
