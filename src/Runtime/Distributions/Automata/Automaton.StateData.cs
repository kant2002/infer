// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.ML.Probabilistic.Distributions.Automata
{
    using System;
    using System.Runtime.Serialization;
    using Microsoft.ML.Probabilistic.Collections;
    using Microsoft.ML.Probabilistic.Serialization;

    public abstract partial class Automaton<TSequence, TElement, TElementDistribution, TSequenceManipulator, TThis>
    {
        /// <summary>
        /// Represents a state of an automaton that is stored in the Automaton.states. This is an internal representation
        /// of the state. <see cref="State"/> struct should be used in public APIs.
        /// </summary>
        [Serializable]
        [DataContract]
        internal class StateData
        {
            [DataMember]
            public ImmutableArraySegment<Transition> Transitions { get; }

            /// <summary>
            /// Gets or sets ending weight of the state.
            /// </summary>
            [DataMember]
            public Weight EndWeight { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="StateData"/> struct.
            /// </summary>
            [Construction("Transitions", "EndWeight")]
            public StateData(ImmutableArraySegment<Transition> transitions, Weight endWeight)
            {
                this.Transitions = transitions;
                this.EndWeight = endWeight;
            }

            /// <summary>
            /// Gets a value indicating whether the ending weight of this state is greater than zero.
            /// </summary>
            internal bool CanEnd => !this.EndWeight.IsZero;
        }
    }
}
