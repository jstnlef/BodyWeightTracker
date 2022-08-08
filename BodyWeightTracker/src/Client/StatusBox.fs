module StatusBox

open Feliz
open Feliz.Bulma

type StatusBoxModel =
    {
        text: string
        subtext: string
    }

let statusBox (model: StatusBoxModel) =
    Bulma.card [
        Bulma.cardContent [
            Bulma.media [
    //            Bulma.mediaLeft [
    //                Bulma.cardImage [
    //                    Bulma.image [
    //                        image.is64x64
    //                        prop.children [
    //                            Html.img [
    //                                prop.alt "Placeholder image"
    //                                prop.src "https://bulma.io/images/placeholders/96x96.png"
    //                            ]
    //                        ]
    //                    ]
    //                ]
    //            ]
                Bulma.mediaContent [
                    Bulma.title.p [
                        title.is4
                        prop.text model.text
                    ]
                    Bulma.subtitle.p [
                        title.is6
                        prop.text model.subtext
                    ]
                ]
            ]
        ]
    ]