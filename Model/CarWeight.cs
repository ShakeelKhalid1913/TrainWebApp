namespace Model
{
    public class CarWeight
    {
        // create properties
        public string? CarId { get; set; }

        public string? Owner { get; set; }

        public RailCarType? CarType { get; set; }

        public int? ScaleWeight { get; set; }

        public string? SerialNumber { get; set; }

        // create constructor
        public CarWeight()
        {

        }

        public CarWeight(string owner, string carid, RailCarType type, int scaleWeight){
          this.Owner = owner;
          this.CarId = carid;
          this.CarType = type;
          this.ScaleWeight = scaleWeight;
        }

        public CarWeight parse(string value){
          return new CarWeight();
        }

        public string toString(){
          return Owner + " " + CarId + ", " + CarType + ", " + ScaleWeight;
        }

        public bool TryParse(string value){
          return true;
        }
    }
}