// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.ML.Probabilistic.Distributions.Automata
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;

    using Microsoft.ML.Probabilistic.Collections;
    using Microsoft.ML.Probabilistic.Serialization;
    using Microsoft.ML.Probabilistic.Utilities;

    /// <content>
    /// TODO
    /// </content>
    public abstract partial class Automaton<TSequence, TElement, TElementDistribution, TSequenceManipulator, TThis>
    {
        public struct TransitionsList : IReadOnlyList<Transition>
        {
            private readonly int baseStateIndex;
            private readonly ImmutableArraySegment<Transition> transitions;

            public TransitionsList(
                int baseStateIndex,
                ImmutableArraySegment<Transition> transitions)
            {
                this.baseStateIndex = baseStateIndex;
                this.transitions = transitions;
            }

            /// <inheritdoc/>
            public Transition this[int index]
            {
                get
                {
                    var result = this.transitions[index];
                    result.DestinationStateIndex += this.baseStateIndex;
                    return result;
                }
            }

            /// <inheritdoc/>
            public int Count => this.transitions.Count;

            public TransitionsEnumerator GetEnumerator() =>
                new TransitionsEnumerator(this);

            /// <inheritdoc/>
            IEnumerator<Transition> IEnumerable<Transition>.GetEnumerator() =>
                this.GetEnumerator();

            /// <inheritdoc/>
            IEnumerator IEnumerable.GetEnumerator() =>
                this.GetEnumerator();

            public struct TransitionsEnumerator : IEnumerator<Transition>
            {
                private readonly int baseStateIndex;
                private ImmutableArraySegmentEnumerator<Transition> enumerator;

                /// <summary>
                /// Initializes a new instance of <see cref="ImmutableArraySegment{T}"/> structure.
                /// </summary>
                internal TransitionsEnumerator(TransitionsList list)
                {
                    this.baseStateIndex = list.baseStateIndex;
                    this.enumerator = list.transitions.GetEnumerator();
                }

                /// <inheritdoc/>
                public void Dispose()
                {
                }

                /// <inheritdoc/>
                public bool MoveNext() => this.enumerator.MoveNext();

                /// <inheritdoc/>
                public Transition Current
                {
                    get
                    {
                        var result = this.enumerator.Current;
                        result.DestinationStateIndex += this.baseStateIndex;
                        return result;
                    }
                }

                /// <inheritdoc/>
                object IEnumerator.Current => this.Current;

                /// <inheritdoc/>
                void IEnumerator.Reset() => this.enumerator.Reset();
            }
        }
    }
}
