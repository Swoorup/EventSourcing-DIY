namespace Step9.Tests

module Domain =
  open Step9.Domain
  open Expecto
  open Expecto.Expect

  let Given = id

  let When handler events =
    handler events

  let Then expectedEvents events =
    equal events expectedEvents "Events should equal expected events"

  let truckTests =
    let truck = Truck <| System.Guid.NewGuid()
    let truck2 = Truck <| System.Guid.NewGuid()

    testList "truckTests"
      [
        test "Truck_added_to_fleet" {
          Given
            [
              Truck_added_to_fleet truck
            ]
          |> When Behaviour.addTruckToFleet truck
          |> Then [Truck_added_to_fleet truck]
        }

        // test "Truck_already_in_fleet" {
        //   Given
        //     [
        //       Truck_added_to_fleet truck
        //     ]
        //   |> When Behaviour.addTruckToFleet truck
        //   |> Then [Truck_already_in_fleet truck]
        // }
      ]


  let sellTests =
    let truck = Truck <| System.Guid.NewGuid()
    let truck2 = Truck <| System.Guid.NewGuid()

    testList "sellFlavour"
      [
        test "Flavour_sold" {
          Given
            [
              Flavour_restocked (truck,Vanilla,5)
              Flavour_sold (truck,Vanilla)
              Flavour_sold (truck,Vanilla)
            ]
          |> When (Behaviour.sellFlavour truck Vanilla)
          |> Then [Flavour_sold (truck,Vanilla)]
        }

        test "Flavour_was_not_in_stock" {
          Given
            [
              Flavour_restocked (truck,Vanilla,5)
              Flavour_restocked (truck,Strawberry,2)
              Flavour_sold (truck,Vanilla)
              Flavour_sold (truck,Vanilla)
              Flavour_sold (truck,Strawberry)
              Flavour_sold (truck,Strawberry)
              Flavour_went_out_of_stock (truck,Strawberry)
            ]
          |> When (Behaviour.sellFlavour truck Strawberry)
          |> Then [Flavour_was_not_in_stock (truck,Strawberry)]
        }

        test "Flavour_restocked" {
          Given []
          |> When (Behaviour.restock truck Vanilla 5)
          |> Then [Flavour_restocked (truck,Vanilla,5)]
        }
      ]

  let domainTests =
    testList "domainTests"
      [
        truckTests
        sellTests
      ]

