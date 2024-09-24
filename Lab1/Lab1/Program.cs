using Lab1.Race;

RaceSetuper initializer = new RaceSetuper();
Race race = initializer.SetupRace();
race.StartRace();


//RaceType userSelection;
//            double distance;
//while (true)
//{
//    Console.WriteLine("Select race type: AIR, GROUND or MIXED");
//    string selected = Console.ReadLine();
//    if (Enum.TryParse(selected, out userSelection))
//    {
//        while (true)
//        {
//            Console.WriteLine("Enter race distance");
//            if (double.TryParse(Console.ReadLine(), out distance))
//            {
//                break;
//            }
//        }
//        break;
//    }
//    else
//    {
//        Console.WriteLine("Try Entering again");
//    }
//}

//Race activeRace = new Race(userSelection, distance);
//List<Type> availableTypes = new List<Type> {
//    typeof(Broom),
//    typeof(BabaYagaMortar),
//    typeof(FlyingShip),
//    typeof(MagicCarpet),
//    typeof(Centaur),
//    typeof(ChickenLegsHut),
//    typeof(MagicBoots),
//    typeof(PumpkinCarriage)
//};


//while (true)
//{
//    Console.WriteLine("Select one of the types to register. Enter -1 to finish.");
//    for (int i = 0; i < availableTypes.Count; i++)
//    {
//        Console.WriteLine($"{i + 1} - {availableTypes[i].Name}");
//    }
//    int choice;
//    if (!int.TryParse(Console.ReadLine(), out choice) || choice < -1 || choice > availableTypes.Count)
//    {
//        Console.WriteLine("Try again");
//        continue;
//    }
//    if (choice == -1)
//    {
//        break;
//    }
//    else
//    {
//        Type selectedVehicleType = availableTypes[choice - 1];
//        Vehicle vehicle = (Vehicle)Activator.CreateInstance(selectedVehicleType);
//        try
//        {
//            activeRace.RegisterVehicle(vehicle);
//            availableTypes.RemoveAt(choice - 1);
//        }
//        catch
//        {
//            Console.WriteLine("Unsupported vehicle");
//        }
//    }
//}

//activeRace.StartRace();