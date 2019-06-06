using System;
using System.Collections.Generic;
using StateMachine;

namespace StateMachine
{
    class Program
    {
        static void Main(string[] args)
        {

            // Range from 0 to 9
            var automata = new FiniteSM<int, int>(
                new Dictionary<int, Func<int, (int, int)>>
                {
                    {0, _   => (1, 0)},
                    {1, val => ++val < 10 ? (1, val) : (-1, 0)},
                },
                0,
                -1
            );

            automata.ForEach(val => Console.WriteLine(val));
        }
    }
}
