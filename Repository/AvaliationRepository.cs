﻿using AvaliaAe.Context;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AvaliaAe.Repository
{
    public class AvaliationRepository : IAvaliationRepository
    {
        private readonly DatabaseContext _context;
        public AvaliationRepository(DatabaseContext context)
        {
            _context = context;
        }
        public AvaliationModel InsertAvaliation(AvaliationModel avaliation)
        {
            _context.Avaliations.Add(avaliation);
            _context.SaveChanges();
            return avaliation;
        }

        public List<AvaliationModel> GetAllComments(int idInst)
        {
            List<AvaliationModel> avList = new List<AvaliationModel>();
            var result = (from c in _context.Avaliations
                          join l in _context.User
                          on c.UserId equals l.Id
                          join i in _context.Institution
                          on c.InstitutionId equals i.Id
                          where i.Id == idInst
                          select new { l.Name, c.Comment, c.Note, i.InstitutionName, l.photo_uri, idAvaliation = c.Id, institutionId = i.Id, userIdentity = l.Id }).OrderByDescending(x => x.idAvaliation);
           foreach(var avaliation in result)
            {
                avList.Add(new AvaliationModel()
                {
                    Id = avaliation.idAvaliation,
                    User = new UserModel()
                    {
                        Id = avaliation.userIdentity,
                        Name = avaliation.Name,
                        photo_uri = avaliation.photo_uri
                    },
                    Institution = new InstitutionModel()
                    {
                        InstitutionName = avaliation.InstitutionName,
                        Id = avaliation.institutionId
                    },
                    Comment = avaliation.Comment,
                    Note = avaliation.Note
                });
            }

            return avList;
        }

        public List<AvaliationModel> allAvaliations()
        {
            List<AvaliationModel> avaliationModel = new List<AvaliationModel>();
            var result = (from i in _context.Avaliations
                          join c in _context.Institution
                          on i.InstitutionId equals c.Id
                          select new { i.Note, c.InstitutionName });
            foreach(var i in result)
            {
                avaliationModel.Add(new AvaliationModel()
                {
                    Note = i.Note,
                    Institution = new InstitutionModel()
                    {
                        InstitutionName = i.InstitutionName,
                    }
                });
            }
            return avaliationModel;
        }

        public AvaliationModel GetAvaliationByUserId(int userId, int idInst)
        {
            var result = (from i in _context.Avaliations
                          join c in _context.User
                          on i.UserId equals c.Id
                          where i.InstitutionId == idInst && i.UserId == userId
                          select i).FirstOrDefault();
            return result;

        }

        public bool RemoveAvaliationAverage(int idInst, int idUser)
        {
            var result = _context.Avaliations
                .FirstOrDefault(x => x.InstitutionId == idInst && x.UserId == idUser);

            if (result == null)
            {
                return false;
            }
            _context.Avaliations.Remove(result);

            _context.SaveChanges();

            return true;
        }
    }
}
