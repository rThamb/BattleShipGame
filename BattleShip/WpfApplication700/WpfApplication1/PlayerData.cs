using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    [Serializable]
    class PlayerData : IComparable<PlayerData>
    {
        private String name;
        private int playersWins;
        private int playersLoses;

        public PlayerData(String name)
        {
            this.name = name;
        }


        public String Name
        {
            get { return name; }
            set { this.name = value; }
        }

        public int Wins
        {
            get { return playersWins; }
        }

        public int Loses
        {
            get { return playersLoses; }
        }

        public void incPlayersWins()
        {
            this.playersWins++;
        }

        public void incPlayersLoses()
        {
            this.playersLoses++;
        }



        public override String ToString()
        {
            return name + "*" + playersWins + "*" + playersLoses;
        }

        public int CompareTo(PlayerData aPlayerData)
        {
            return this.name.CompareTo(aPlayerData.name);
        }
    }
}
