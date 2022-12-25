namespace HW3_server.Models
{
    public class Flat
    {
        //fileds
        int id;
        string city;
        string address;
        double price;
        int numberOfRooms;


        //properties
        public int Id { get => id; set => id = value; }
        public string City { get => city; set => city = value; }
        public string Address { get => address; set => address = value; }
        public int NumberOfRooms { get => numberOfRooms; set => numberOfRooms = value; }
        public double Price { get => price; set => price = Discount(value); }

        //methods
        public bool Insert() //פונקציה שמכניסה דירה לרשימת דירות במידה והדירה לא קיימת עדיין במילון
        {
            DBservices dbs = new DBservices();
            List<Flat> FlatList = dbs.ReadFlats();
       
            foreach (Flat flat in FlatList) //בדיקה האם הדירה קיימת
            {
                if (this.Id == flat.Id)
                {
                    return false;
                }
            }
            dbs.InsertFlat(this);
            return true; 
        }

        public List<Flat> Read() //פונקציה שמציגה את כל האיברים במילון הדירות
        { 
            DBservices dbs = new DBservices();
            return dbs.ReadFlats();
        }

        public double Discount(double price) //פונקציה שבודקת האם המחיר שהוכנס עומד ב2 תנאים ואם כן מחזיר את המחיר לאחר הנחה
        {
            if (this.NumberOfRooms > 1 && price > 100.0)
            {
                price *= 0.9;
            }
            return price;
        }

        public static List<Flat> getCityPrice(string city, double price)//פונקציה המחזירה את כל הדירות שנמצאות באותו העיר שהתקבלה ובמחיר מתחת למחיר הרצוי
        {
            DBservices dbs = new DBservices();
            List<Flat> FlatList = dbs.ReadFlats();
            List<Flat> newList = new List<Flat>();

            foreach (Flat item in FlatList)
            {
                if (city == item.City && item.Price <= price)
                {
                    newList.Add(item);
                }
            }
            return newList;
        }
    }
}


