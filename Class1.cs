using System;

namespace PrefixesLib
{
    public struct Prefix
    {
        public uint Base;
        public char Mask;
    }

    public struct PrefixSet
    {
        public Prefix[] Prefixes;
        public int Size;
    }

    public class PrefixManager
    {
        private const int MAX_PREFIXES = 64;

        public PrefixSet prefixSet;

        public PrefixManager()
        {
            prefixSet = new PrefixSet
            {
                Prefixes = new Prefix[MAX_PREFIXES],
                Size = 0
            };
        }

        public int Add(uint @base, char mask)
        {
            if (@base < 0 || @base > 4294967295)
            {
                return -1;
            }
            if ((int)(mask) < 0 || (int)(mask) > 32)
            {
                Console.WriteLine("wrong mask");
                return -1;
            }

            for (int i = 0; i < prefixSet.Size; i++) {
                if (prefixSet.Prefixes[i].Base == @base &&
                        prefixSet.Prefixes[i].Mask == mask) {
                    return -1;
                }
            }

            if (prefixSet.Size < MAX_PREFIXES)
            {
                prefixSet.Prefixes[prefixSet.Size].Base = @base;
                prefixSet.Prefixes[prefixSet.Size].Mask = mask;
                prefixSet.Size++;

                return 0;
            }
            else
            {
                return -1;
            }
        }

        public int Delete(uint @base, char mask)
        {
            if (@base < 0 || @base > 4294967295)
            {
                return -1;
            }

            if ((int)(mask) < 0 || (int)(mask) > 32)
            {
                return -1;
            }

            if (prefixSet.Size > 0)
            {
                for (int i = 0; i < prefixSet.Size; i++)
                {
                    if (prefixSet.Prefixes[i].Base == @base &&
                        prefixSet.Prefixes[i].Mask == mask)
                    {
                        for (int j = i; j < prefixSet.Size - 1; j++)
                        {
                            prefixSet.Prefixes[j] = prefixSet.Prefixes[j + 1];
                        }
                        prefixSet.Size--;
                        return 0;
                    }
                }
            }
            return -1;
        }

        public int Check(uint ip)
        {
            int maxMask = -1;

            for (int i = 0; i < prefixSet.Size; i++)
            {
                if (ip == prefixSet.Prefixes[i].Base)
                {
                    Console.WriteLine("takie same maski");
                    if (prefixSet.Prefixes[i].Mask > maxMask)
                    {
                        maxMask = prefixSet.Prefixes[i].Mask;
                    }
                }
            }

            return maxMask;
        }
    }
}
