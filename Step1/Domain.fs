namespace Step1.Domain

type Flavour =
  | Vanilla
  | Strawberry

type Event =
  | IcecreamSold of Flavour
  | Icecream_Restocked of Flavour * int
  | Flavour_empty of Flavour
  | Flavour_was_not_in_stock of Flavour