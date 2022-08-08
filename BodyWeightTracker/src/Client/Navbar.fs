module Navbar

open Feliz
open Feliz.Bulma

let navbar =
    Bulma.navbar [
        Bulma.navbarMenu [
            Bulma.navbarStart.div [
                Bulma.navbarItem.a [ prop.text "Home" ]
                Bulma.navbarItem.div [
                    navbarItem.hasDropdown
                    navbarItem.isHoverable
                    prop.children [
                        Bulma.navbarLink.a [ prop.text "More" ]
                        Bulma.navbarDropdown.div [
                            Bulma.navbarItem.a [ prop.text "About" ]
                            Bulma.navbarItem.a [
                                prop.text "Contact"
                            ]
                            Bulma.navbarDivider []
                            Bulma.navbarItem.a [
                                prop.text "Report a issue"
                            ]
                        ]
                    ]
                ]
            ]
            Bulma.navbarEnd.div [
                Bulma.navbarItem.div [
                    Bulma.buttons [
                        Bulma.button.a [
                            Bulma.color.isPrimary
                            prop.children [ Html.strong "Sign up" ]
                        ]
                        Bulma.button.a [ prop.text "Log In" ]
                    ]
                ]
            ]
        ]
    ]
