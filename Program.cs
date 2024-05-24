namespace bridge_pattern
{
    //A demonstration of the Bridge pattern in C#
    internal class Program
    {
        static void Main(string[] args)
        {
            Make subaruMake = new SubaruMake();
            Make fordMake = new FordMake();

            Car subaruCar = new Car(subaruMake);
            Truck fordTruck = new Truck(fordMake);

            Console.WriteLine(subaruCar.IsAllowedToDrive(6));
            Console.WriteLine(fordTruck.IsAllowedToDrive(5));
        }
    }

    //Top Level Abstraction
    public abstract class Vehicle { }

    //Implementation
    public class Subaru : Vehicle { }
    public class Ford : Vehicle { }

    //Lower Level Abstraction
    public class SubaruCar : Vehicle { }
    public class SubaruTruck : Vehicle { }
    public class FordCar : Vehicle { }
    public class FordTruck : Vehicle { }

    /*
     * ISSUE: For every new make we would need to create three new classes; the <Make> class and the Car and Truck classes
     *          It isn't scalable.
     */

    //Instead, we could abstract out the Vehicle part of it, as well as the Make since they should have no overlap

    public abstract class NewVehicle
    {
        //Each vehicle type now has a Make, regardless of whether it's a Car ot Truck
        protected Make make;

        public void Start()
        {
            make.Start();
        }

        //Each type of vehicle could need a different license to drive - but why have this in each implementation
        public abstract bool IsAllowedToDrive(int licenseLevel);
    }

    public class Car : NewVehicle
    {
        public Car (Make make)
        {
            this.make = make;
        }

        //We only need to implement this per vehicle type now, instead of having this in every Vehicle Type + Make combination
        public override bool IsAllowedToDrive(int licenseLevel)
        {
            return licenseLevel == 7;
        }
    }

    public class Truck : NewVehicle
    {
        public Truck(Make make)
        {
            this.make = make;
        }

        public override bool IsAllowedToDrive(int licenseLevel)
        {
            return licenseLevel == 5;
        }
    }

    public abstract class Make
    {
        //Each make may have a different way to start the vehicle
        public abstract void Start();
    }

    public class SubaruMake : Make
    {
        public override void Start()
        {
            Console.WriteLine(
                "Grab key fob \n",
                "Hold brake \n",
                "Press start button"
                );
        }
    }

    public class FordMake : Make
    {
        public override void Start()
        {
            Console.WriteLine(
                "Unlock door \n",
                "Turn key \n",
                "Get out of Park"
                );
        }
    }
}
