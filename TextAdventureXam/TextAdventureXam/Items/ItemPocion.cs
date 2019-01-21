﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureXam
{
    class ItemPocion : ItemConsumable
    {
        public enum PocionType
        {
            hp, mana
        }

        int cantidad;
        PocionType tipo;

        public ItemPocion(string name, int cantidad, PocionType tipo) : base(name)
        {
            this.cantidad = cantidad;
            this.tipo = tipo;
        }

        public override void Consumir()
        {
            Player pl = Program.ObteJuego().pl;
            int recu = GetRecoveryStat();
            if (GetPocionType() == PocionType.hp)
                pl.RestoreHealth(recu);
            else
                pl.RestoreMana(recu);
        }

        public int GetRecoveryStat()
        {
            if (GetPocionType() == PocionType.hp)
                return Program.ObteJuego().pl.GetMHealth() * cantidad / 100;
            else
                return Program.ObteJuego().pl.GetManaM() * cantidad / 100;
        }

        public int GetFlatCant()
        {
            return cantidad;
        }

        public PocionType GetPocionType()
        {
            return tipo;
        }
    }
}
