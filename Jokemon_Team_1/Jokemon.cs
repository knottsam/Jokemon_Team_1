﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jokemon_Team_1
{
    class Jokemon : Sprite
    {
        public int health { get; set; }
        public int catchrate { get; set; }
        public JokemonSkills skill1 { get; set; }
        public JokemonSkills skill2 { get; set; }
        public JokemonSkills skill3 { get; set; }
        public JokemonSkills skill4 { get; set; }

        public Jokemon(Texture2D tex, Vector2 pos, Vector2 size) : base(tex, pos, size)
        {

        }

        public JokemonSkills UsingSkill1(JokemonSkills theskill)
        {
            return skill1;  // might not need
        }


    }
}
