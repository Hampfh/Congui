// <copyright file="EventHandler.cs" company="Hampfh and haholm">
// Copyright (c) Hampfh and haholm. All rights reserved.
// </copyright>

namespace Congui.Events {
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;

    /// <summary>
    /// Manages and handles events.
    /// </summary>
    public static class EventHandler {
        /// <summary>
        /// The maximum number of events that can be registered.
        /// </summary>
        public const int MaximumNumberOfEvents = 10;

        private static ConcurrentBag<EventParameters> eventBag = new ConcurrentBag<EventParameters>();
        private static Task eventTask = new Task(EventLoop);

        /// <summary>
        /// Gets a value indicating how many events have been registered.
        /// </summary>
        /// <value>A value indicating how many events have been registered.</value>
        public static int NumberOfEvents {
            get {
                return eventBag.Count;
            }
        }

        /// <summary>
        /// Register an event to be handled.
        /// </summary>
        /// <param name="eventParameters">A <see cref="EventParameters"/> object defining event information.</param>
        public static void RegisterEvent(EventParameters eventParameters) {
            if (eventBag.Count == MaximumNumberOfEvents) {
                throw new OverflowException("Can not register another event. The maximum number of events possible to register was reached.");
            }

            eventBag.Add(eventParameters);
            if (eventTask.Status == TaskStatus.Created) {
                eventTask.Start();
            }
        }

        private static void EventLoop() {
            var options = new ParallelOptions {
                MaxDegreeOfParallelism = Environment.ProcessorCount,
            };
            while (true) {
                Parallel.ForEach(
                    source: eventBag,
                    parallelOptions: options,
                    body: (eventParameters, loopState, something) => {
                        if (eventParameters.Condition()) {
                            eventParameters.SubscribingMethod();
                        }
                    });
            }
        }
    }
}