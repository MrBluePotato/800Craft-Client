﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ManicDigger
{
    public class IterableSet<T>
    {
        public Dictionary<T, int> dictionary = new Dictionary<T, int>();
        public List<T> list = new List<T>();
        public Dictionary<int, bool> free = new Dictionary<int, bool>();

        public int curpos;
        public IEnumerable<T> Iterate(int max)
        {
            int max2 = Math.Min(max, list.Count);
            for (int i = 0; i < max2; i++)
            {
                curpos++;
                curpos = curpos % list.Count;
                if (!free.ContainsKey(curpos))
                {
                    yield return list[curpos];
                }
            }
        }
        public void Add(T value)
        {
            if (!dictionary.ContainsKey(value))
            {
                if (free.Count > 0)
                {
                    int pos = System.Linq.Enumerable.First(free.Keys);
                    free.Remove(pos);
                    list[pos] = value;
                    dictionary[value] = pos;
                }
                else
                {
                    list.Add(value);
                    dictionary[value] = list.Count - 1;
                }
            }
        }
        public void Remove(T value)
        {
            if (dictionary.ContainsKey(value))
            {
                int pos = dictionary[value];
                dictionary.Remove(value);
                //list.Remove(value);
                free[pos] = true;
            }
        }
    }
}