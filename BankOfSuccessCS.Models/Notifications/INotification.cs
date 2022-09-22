﻿namespace BankOfSuccessCS.Models
{
    public interface INotification
    {
        void Update(string message);
        void Update(Transaction t);
    }

    public interface INotify
    {
        void Subscribe(INotification notification);
        void UnSubscribe(INotification notification);
        void Notify(string message);
        void Notify(Transaction t);
    }
}
