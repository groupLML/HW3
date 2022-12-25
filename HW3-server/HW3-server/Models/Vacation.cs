using System;

namespace HW3_server.Models
{
    public class Vacation
    {
        //fileds
        int id;
        string userId;
        int flatId;
        DateTime startDate;
        DateTime endDate;

        //properties
        public int Id { get => id; set => id = value; }
        public string UserId { get => userId; set => userId = value; }
        public int FlatId { get => flatId; set => flatId = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }

        //methods
        public bool Insert()
        {
            DBservices dbs = new DBservices();
            List<Vacation> OrdersList = dbs.ReadOrders();
            List<Flat> FlatList = dbs.ReadFlats();

            foreach (Vacation vacation in OrdersList) //בדיקה האם ההזמנה קיימת
            {
                if (this.Id == vacation.Id)
                {
                    return false;
                }
            }

            foreach (Flat flat in FlatList) //בדיקה האם הדירה קיימת
            {
                if (this.FlatId == flat.Id)
                {
                    if (EndDate < StartDate || StartDate < DateTime.Today) //בדיקת תקינות התאריכים
                    {
                        return false;
                    }

                    foreach (Vacation vacation in OrdersList) //בדיקה האם התאריכים פנויים עבור דירה מסויימת
                    {
                        if (FlatId == vacation.FlatId)
                        {
                            if (StartDate <= vacation.EndDate && EndDate >= vacation.StartDate)
                            {
                                return false;
                            }
                        }
                    }
                    dbs.InsertVacation(this);
                    return true;
                }
            }
            return false;
        }

        public List<Vacation> Read() //פונקציה המחזירה את כל פרטי רשימת ההזמנות
        {
            DBservices dbs = new DBservices();
            return dbs.ReadOrders();
        }

        public static List<Vacation> getByDates(DateTime start, DateTime end)// פונקציה שמחזירה את החופשות בין הטווח תאריכים שהתקבלו
        {
            DBservices dbs = new DBservices();
            List<Vacation> OrdersList = dbs.ReadOrders();
            List<Vacation> newList = new List<Vacation>();

            foreach (Vacation vacation in OrdersList)
            {
                if (start <= vacation.startDate && vacation.endDate <= end)
                {
                    newList.Add(vacation);
                }
            }
            return newList;
        }
    }
}
