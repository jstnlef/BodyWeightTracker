module AddWeightModal

open System
open Feliz
open Feliz.Bulma


let modal =
  // let (modalState, toggleState) = Feliz.React.useState (false)

  Bulma.modal [
    prop.id "add-weight-modal"
    Bulma.modal.isActive
    // if modalState then Bulma.modal.isActive
    prop.children [
      Bulma.modalBackground []
      Bulma.modalContent [
        Bulma.box [
          Html.form [
            Bulma.field.div [
              Bulma.label "Date"
              DateTimePicker.dateTimePicker [
                dateTimePicker.dateOnly true
                dateTimePicker.defaultValue DateTime.Today
                dateTimePicker.onDateRangeSelected (fun (d: (DateTime * DateTime) option) -> () (* handle here *) )
                dateTimePicker.isRange false
                dateTimePicker.closeOnSelect true
              ]
            ]
            Bulma.field.div [
              Bulma.label "Weight"
              Bulma.control.div [
                Bulma.input.text []
              ]
            ]
            Bulma.field.div [
              Bulma.label "Body fat percentage"
              Bulma.control.div [
                Bulma.input.text []
              ]
            ]
            Bulma.field.div [
              Bulma.field.isGrouped
              Bulma.field.isGroupedCentered
              prop.children [
                Bulma.control.div [
                  Bulma.button.button [
                    Bulma.color.isLink
                    prop.text "Submit"
                  ]
                ]
              ]
            ]
          ]
        ]
      ]
      Bulma.modalClose [
      // prop.onClick (fun _ -> toggleState (false))
      ]
    ]
  ]
