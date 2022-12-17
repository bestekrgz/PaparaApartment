using PaparaApartment.Core.Entities;
using System;
using MongoDB.Bson;


namespace PaparaApartment.Entity.Concrete
{
    public class Payment : IEntity
    {
        public ObjectId Id { get; set; }
        public int ApartmentId { get; set; }
        public int ExpenseId { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string CardCVC { get; set; }
        public string CardValDate { get; set; }
        public string Type { get; set; }
        public string ExpenseName { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
