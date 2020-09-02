using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Texts : MonoBehaviour
{
    protected string[] textsLanguage;
    protected string[] initializeComands = { };
    protected string[] comands = { };
    protected string[] textsMenu;
    public Text[] textsInMenu;
    protected void TextsTutoria()
    {
        if (!Menu.language)
        {
            initializeComands = new string[]{
                "Vamos lá, paciência que será só uma vez",
                "Primeiramente, você deve proteger a casa",
                "cada pessoa nela possui um tipo de ataque especifico",
                "e você terá que controlar a posição deles",

                "otimo, dessa forma vocẽ pode controlar as posições",
                "outra coisa é a barra de estamina abaixo da imagem dos personagens",
                "ela vai diminuindo enquanto a pessoa estiver atacando",

                "Isso, Dessa forma a pessoa poderá recuperar a estamina dela",
                "caso queira colocar ela de volta, basta clicar nela e depois clicar na posição em que ela estava",
                "Agora, proteja a casa"
            };

            comands = new string[] {
                "toque no membro da familia",
                "toque no outro membro da familia",
                "toque no sofa"
            };

            textsLanguage = new string[] {
                "Você Perdeu",
            "Jogar",
            "Voltar",
            "Deseja fazer o tutorial?",
            "sim",
            "nao"
            };

        }
        else
        {
            initializeComands = new string[]{
                "Come on, pacience tha will be one time",
                "first, you need to protect the house",
                "each person has a specific attack",
                "and you will have to control their position",

                "great, that way you can control the positions",
                "another important thing is the stamina bar below the image of the characters",
                "it decreases while the person is attacking",

                "that way the person will be able to recover stamina",
                "if you want to put it back, just click on it and click on the position in which it was",
                "now, protect the house"
            };

            comands = new string[] {
                "touch the person",
                "touch the other person",
                "touch the couch"
            };

            textsLanguage = new string[] {
                "You Lose",
            "Restart",
            "Menu",
            "You need tutotial?",
            "yes",
            "no"
            };

        }

    }
    protected void TextLanguageMethod(bool language)
    {

        if (!language)
        {
            textsMenu = new string[] {
                "Jogar",
                "Opções",
                "Sair",
                "Idioma",
                "som",
                "Voltar"
                };
        }
        else
        {
            textsMenu = new string[] {
                "PLAY",
                "OPTIONS",
                "EXIT",
                "IDIOM",
                "SOUND",
                "BACK"
                };
        }
        for (int i = 0; i < textsMenu.Length; i++)
        {
            textsInMenu[i].text = textsMenu[i];
        }
    }
}
