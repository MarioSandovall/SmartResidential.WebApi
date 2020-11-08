namespace Domain.Entities
{
    public class HouseUserEf
    {

        public int HouseId { get; set; }

        public HouseEf House { get; set; }

        public int UserId { get; set; }

        public UserEf User { get; set; }

    }
}
