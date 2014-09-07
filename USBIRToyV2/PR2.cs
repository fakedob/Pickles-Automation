using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace USBIRToyv2
{
    public class PR2
    {
        private static Dictionary<decimal, byte> GetPR2Codes()
        {
            Dictionary<decimal, byte> PR2 = new Dictionary<decimal, byte>();
            PR2.Add(11718.75m, 255);
            PR2.Add(11764.71m, 127);
            PR2.Add(11811.02m, 191);
            PR2.Add(11857.71m, 63);
            PR2.Add(11904.76m, 223);
            PR2.Add(11952.19m, 95);
            PR2.Add(12000.00m, 159);
            PR2.Add(12048.19m, 31);
            PR2.Add(12096.77m, 239);
            PR2.Add(12145.75m, 111);
            PR2.Add(12195.12m, 175);
            PR2.Add(12244.90m, 47);
            PR2.Add(12295.08m, 207);
            PR2.Add(12345.68m, 79);
            PR2.Add(12396.69m, 143);
            PR2.Add(12448.13m, 15);
            PR2.Add(12500.00m, 247);
            PR2.Add(12552.30m, 119);
            PR2.Add(12605.04m, 183);
            PR2.Add(12658.23m, 55);
            PR2.Add(12711.86m, 215);
            PR2.Add(12765.96m, 87);
            PR2.Add(12820.51m, 151);
            PR2.Add(12875.54m, 23);
            PR2.Add(12931.03m, 231);
            PR2.Add(12987.01m, 103);
            PR2.Add(13043.48m, 167);
            PR2.Add(13100.44m, 39);
            PR2.Add(13157.89m, 199);
            PR2.Add(13215.86m, 71);
            PR2.Add(13274.34m, 135);
            PR2.Add(13333.33m, 7);
            PR2.Add(13392.86m, 251);
            PR2.Add(13452.91m, 123);
            PR2.Add(13513.51m, 187);
            PR2.Add(13574.66m, 59);
            PR2.Add(13636.36m, 219);
            PR2.Add(13698.63m, 91);
            PR2.Add(13761.47m, 155);
            PR2.Add(13824.88m, 27);
            PR2.Add(13888.89m, 235);
            PR2.Add(13953.49m, 107);
            PR2.Add(14018.69m, 171);
            PR2.Add(14084.51m, 43);
            PR2.Add(14150.94m, 203);
            PR2.Add(14218.01m, 75);
            PR2.Add(14285.71m, 139);
            PR2.Add(14354.07m, 11);
            PR2.Add(14423.08m, 243);
            PR2.Add(14492.75m, 115);
            PR2.Add(14563.11m, 179);
            PR2.Add(14634.15m, 51);
            PR2.Add(14705.88m, 211);
            PR2.Add(14778.33m, 83);
            PR2.Add(14851.49m, 147);
            PR2.Add(14925.37m, 19);
            PR2.Add(15000.00m, 227);
            PR2.Add(15075.38m, 99);
            PR2.Add(15151.52m, 163);
            PR2.Add(15228.43m, 35);
            PR2.Add(15306.12m, 195);
            PR2.Add(15384.62m, 67);
            PR2.Add(15463.92m, 131);
            PR2.Add(15544.04m, 3);
            PR2.Add(15625.00m, 253);
            PR2.Add(15706.81m, 125);
            PR2.Add(15789.47m, 189);
            PR2.Add(15873.02m, 61);
            PR2.Add(15957.45m, 221);
            PR2.Add(16042.78m, 93);
            PR2.Add(16129.03m, 157);
            PR2.Add(16216.22m, 29);
            PR2.Add(16304.35m, 237);
            PR2.Add(16393.44m, 109);
            PR2.Add(16483.52m, 173);
            PR2.Add(16574.59m, 45);
            PR2.Add(16666.67m, 205);
            PR2.Add(16759.78m, 77);
            PR2.Add(16853.93m, 141);
            PR2.Add(16949.15m, 13);
            PR2.Add(17045.45m, 245);
            PR2.Add(17142.86m, 117);
            PR2.Add(17241.38m, 181);
            PR2.Add(17341.04m, 53);
            PR2.Add(17441.86m, 213);
            PR2.Add(17543.86m, 85);
            PR2.Add(17647.06m, 149);
            PR2.Add(17751.48m, 21);
            PR2.Add(17857.14m, 229);
            PR2.Add(17964.07m, 101);
            PR2.Add(18072.29m, 165);
            PR2.Add(18181.82m, 37);
            PR2.Add(18292.68m, 197);
            PR2.Add(18404.91m, 69);
            PR2.Add(18518.52m, 133);
            PR2.Add(18633.54m, 5);
            PR2.Add(18750.00m, 249);
            PR2.Add(18867.92m, 121);
            PR2.Add(18987.34m, 185);
            PR2.Add(19108.28m, 57);
            PR2.Add(19230.77m, 217);
            PR2.Add(19354.84m, 89);
            PR2.Add(19480.52m, 153);
            PR2.Add(19607.84m, 25);
            PR2.Add(19736.84m, 233);
            PR2.Add(19867.55m, 105);
            PR2.Add(20000.00m, 169);
            PR2.Add(20134.23m, 41);
            PR2.Add(20270.27m, 201);
            PR2.Add(20408.16m, 73);
            PR2.Add(20547.95m, 137);
            PR2.Add(20689.66m, 9);
            PR2.Add(20833.33m, 241);
            PR2.Add(20979.02m, 113);
            PR2.Add(21126.76m, 177);
            PR2.Add(21276.60m, 49);
            PR2.Add(21428.57m, 209);
            PR2.Add(21582.73m, 81);
            PR2.Add(21739.13m, 145);
            PR2.Add(21897.81m, 17);
            PR2.Add(22058.82m, 225);
            PR2.Add(22222.22m, 97);
            PR2.Add(22388.06m, 161);
            PR2.Add(22556.39m, 33);
            PR2.Add(22727.27m, 193);
            PR2.Add(22900.76m, 65);
            PR2.Add(23076.92m, 129);
            PR2.Add(23255.81m, 1);
            PR2.Add(23437.50m, 254);
            PR2.Add(23622.05m, 126);
            PR2.Add(23809.52m, 190);
            PR2.Add(24000.00m, 62);
            PR2.Add(24193.55m, 222);
            PR2.Add(24390.24m, 94);
            PR2.Add(24590.16m, 158);
            PR2.Add(24793.39m, 30);
            PR2.Add(25000.00m, 238);
            PR2.Add(25210.08m, 110);
            PR2.Add(25423.73m, 174);
            PR2.Add(25641.03m, 46);
            PR2.Add(25862.07m, 206);
            PR2.Add(26086.96m, 78);
            PR2.Add(26315.79m, 142);
            PR2.Add(26548.67m, 14);
            PR2.Add(26785.71m, 246);
            PR2.Add(27027.03m, 118);
            PR2.Add(27272.73m, 182);
            PR2.Add(27522.94m, 54);
            PR2.Add(27777.78m, 214);
            PR2.Add(28037.38m, 86);
            PR2.Add(28301.89m, 150);
            PR2.Add(28571.43m, 22);
            PR2.Add(28846.15m, 230);
            PR2.Add(29126.21m, 102);
            PR2.Add(29411.76m, 166);
            PR2.Add(29702.97m, 38);
            PR2.Add(30000.00m, 198);
            PR2.Add(30303.03m, 70);
            PR2.Add(30612.24m, 134);
            PR2.Add(30927.84m, 6);
            PR2.Add(31250.00m, 250);
            PR2.Add(31578.95m, 122);
            PR2.Add(31914.89m, 186);
            PR2.Add(32258.06m, 58);
            PR2.Add(32608.70m, 218);
            PR2.Add(32967.03m, 90);
            PR2.Add(33333.33m, 154);
            PR2.Add(33707.87m, 26);
            PR2.Add(34090.91m, 234);
            PR2.Add(34482.76m, 106);
            PR2.Add(34883.72m, 170);
            PR2.Add(35294.12m, 42);
            PR2.Add(35714.29m, 202);
            PR2.Add(36144.58m, 74);
            PR2.Add(36585.37m, 138);
            PR2.Add(37037.04m, 10);
            PR2.Add(37500.00m, 242);
            PR2.Add(37974.68m, 114);
            PR2.Add(38461.54m, 178);
            PR2.Add(38961.04m, 50);
            PR2.Add(39473.68m, 210);
            PR2.Add(40000.00m, 82);
            PR2.Add(40540.54m, 146);
            PR2.Add(41095.89m, 18);
            PR2.Add(41666.67m, 226);
            PR2.Add(42253.52m, 98);
            PR2.Add(42857.14m, 162);
            PR2.Add(43478.26m, 34);
            PR2.Add(44117.65m, 194);
            PR2.Add(44776.12m, 66);
            PR2.Add(45454.55m, 130);
            PR2.Add(46153.85m, 2);
            PR2.Add(46875.00m, 252);
            PR2.Add(47619.05m, 124);
            PR2.Add(48387.10m, 188);
            PR2.Add(49180.33m, 60);
            PR2.Add(50000.00m, 220);
            PR2.Add(50847.46m, 92);
            PR2.Add(51724.14m, 156);
            PR2.Add(52631.58m, 28);
            PR2.Add(53571.43m, 236);
            PR2.Add(54545.45m, 108);
            PR2.Add(55555.56m, 172);
            PR2.Add(56603.77m, 44);
            PR2.Add(57692.31m, 204);
            PR2.Add(58823.53m, 76);
            PR2.Add(60000.00m, 140);
            PR2.Add(61224.49m, 12);
            PR2.Add(62500.00m, 244);
            PR2.Add(63829.79m, 116);
            PR2.Add(65217.39m, 180);
            PR2.Add(66666.67m, 52);
            PR2.Add(68181.82m, 212);
            PR2.Add(69767.44m, 84);
            PR2.Add(71428.57m, 148);
            PR2.Add(73170.73m, 20);
            PR2.Add(75000.00m, 228);
            PR2.Add(76923.08m, 100);
            PR2.Add(78947.37m, 164);
            PR2.Add(81081.08m, 36);
            PR2.Add(83333.33m, 196);
            PR2.Add(85714.29m, 68);
            PR2.Add(88235.29m, 132);
            PR2.Add(90909.09m, 4);
            PR2.Add(93750.00m, 248);
            PR2.Add(96774.19m, 120);
            PR2.Add(100000.00m, 184);
            PR2.Add(103448.28m, 56);
            PR2.Add(107142.86m, 216);
            PR2.Add(111111.11m, 88);
            PR2.Add(115384.62m, 152);
            PR2.Add(120000.00m, 24);
            PR2.Add(125000.00m, 232);
            PR2.Add(130434.78m, 104);
            PR2.Add(136363.64m, 168);
            PR2.Add(142857.14m, 40);
            PR2.Add(150000.00m, 200);
            PR2.Add(157894.74m, 72);
            PR2.Add(166666.67m, 136);
            PR2.Add(176470.59m, 8);
            PR2.Add(187500.00m, 240);
            PR2.Add(200000.00m, 112);
            PR2.Add(214285.71m, 176);
            PR2.Add(230769.23m, 48);
            PR2.Add(250000.00m, 208);
            PR2.Add(272727.27m, 80);
            PR2.Add(300000.00m, 144);
            PR2.Add(333333.33m, 16);
            PR2.Add(375000.00m, 224);
            PR2.Add(428571.43m, 96);
            PR2.Add(500000.00m, 160);
            PR2.Add(600000.00m, 32);
            PR2.Add(750000.00m, 192);
            PR2.Add(1000000.00m, 64);
            PR2.Add(1500000.00m, 128);
            PR2.Add(3000000.00m, 0);
            return PR2;
        }
        public static byte GetPR2Code(decimal FrequencyValue)
        {
            byte Result = 0;
            decimal Diff = 0;
            foreach (KeyValuePair<decimal, byte> pr2 in GetPR2Codes())
            {
                if (Diff == 0)
                {
                    if (FrequencyValue > pr2.Key)
                    {
                        Diff = FrequencyValue - pr2.Key;
                    }
                    else
                    {
                        Diff = pr2.Key - FrequencyValue;
                    }
                    Result = pr2.Value;
                }
                else
                {
                    if (FrequencyValue > pr2.Key)
                    {
                        decimal TempDiff = FrequencyValue - pr2.Key;
                        if (TempDiff < Diff)
                        {
                            Result = pr2.Value;
                            Diff = TempDiff;
                        }
                    }
                    else
                    {
                        decimal TempDiff = pr2.Key - FrequencyValue;
                        if (TempDiff < Diff)
                        {
                            Result = pr2.Value;
                            Diff = TempDiff;
                        }
                    }
                }
            }
            return Result;
        }
    }
}
