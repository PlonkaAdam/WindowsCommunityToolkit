// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using Microsoft.System;
using Microsoft.UI.Xaml.Core;
using Windows.ApplicationModel.Core;

namespace Microsoft.Toolkit.Uwp.Helpers
{
    /// <summary>
    /// This class provides static methods helper for executing code in UI thread of the main window.
    /// </summary>
    public static class DispatcherHelper
    {
        /// <summary>
        /// Executes the given function asynchronously on UI thread of the main view.
        /// </summary>
        /// <typeparam name="T">Returned data type of the function</typeparam>
        /// <param name="function">Asynchronous function to be executed asynchronously on UI thread</param>
        /// <param name="priority">Dispatcher execution priority, default is normal</param>
        /// <returns>Awaitable Task with type <typeparamref name="T"/></returns>
        public static Task<T> ExecuteOnUIThreadAsync<T>(Func<Task<T>> function, DispatcherQueuePriority priority = DispatcherQueuePriority.Normal)
        {
            // return ExecuteOnUIThreadAsync<T>(CoreApplication.MainView, function, priority);
            return DispatcherQueue.GetForCurrentThread().AwaitableRunAsync(function, priority);
        }

        ///// <summary>
        ///// Executes the given function asynchronously on given view's UI thread. Default view is the main view.
        ///// </summary>
        ///// <typeparam name="T">Returned data type of the function</typeparam>
        ///// <param name="viewToExecuteOn">View for the <paramref name="function"/>  to be executed on </param>
        ///// <param name="function">Asynchronous function to be executed asynchronously on UI thread</param>
        ///// <param name="priority">Dispatcher execution priority, default is normal</param>
        ///// <returns>Awaitable Task with type <typeparamref name="T"/></returns>
        // public static Task<T> ExecuteOnUIThreadAsync<T>(this CoreApplicationView viewToExecuteOn, Func<Task<T>> function, DispatcherQueuePriority priority = DispatcherQueuePriority.Normal)
        // {
        //    if (viewToExecuteOn == null)
        //    {
        //        throw new ArgumentNullException(nameof(viewToExecuteOn));
        //    }

        // return viewToExecuteOn.DispatcherQueue.AwaitableRunAsync<T>(function, priority);
        // }

        ///// <summary>
        ///// Executes the given function asynchronously on given view's UI thread. Default view is the main view.
        ///// </summary>
        ///// <param name="viewToExecuteOn">View for the <paramref name="function"/>  to be executed on </param>
        ///// <param name="function">Asynchronous function to be executed asynchronously on UI thread</param>
        ///// <param name="priority">Dispatcher execution priority, default is normal</param>
        ///// <returns>Awaitable Task</returns>
        // public static Task ExecuteOnUIThreadAsync(this CoreApplicationView viewToExecuteOn, Func<Task> function, DispatcherQueuePriority priority = DispatcherQueuePriority.Normal)
        // {
        //    if (viewToExecuteOn == null)
        //    {
        //        throw new ArgumentNullException(nameof(viewToExecuteOn));
        //    }

        // return viewToExecuteOn.DispatcherQueue.AwaitableRunAsync(function, priority);
        // }

        /// <summary>
        /// Executes the given function asynchronously on UI thread of the main view.
        /// </summary>
        /// <param name="function">Asynchronous function to be executed asynchronously on UI thread</param>
        /// <param name="priority">Dispatcher execution priority, default is normal</param>
        /// <returns>Awaitable Task</returns>
        public static Task ExecuteOnUIThreadAsync(Func<Task> function, DispatcherQueuePriority priority = DispatcherQueuePriority.Normal)
        {
            // return ExecuteOnUIThreadAsync(CoreApplication.MainView, function, priority);
            return DispatcherQueue.GetForCurrentThread().AwaitableRunAsync(function, priority);
        }

        ///// <summary>
        ///// Executes the given function asynchronously on given view's UI thread. Default view is the main view.
        ///// </summary>
        ///// <param name="viewToExecuteOn">View for the <paramref name="function"/>  to be executed on </param>
        ///// <param name="function">Asynchronous function to be executed asynchronously on UI thread</param>
        ///// <param name="priority">Dispatcher execution priority, default is normal</param>
        ///// <returns>Awaitable Task/></returns>
        // public static Task ExecuteOnUIThreadAsync(this CoreApplicationView viewToExecuteOn, Action function, DispatcherQueuePriority priority = DispatcherQueuePriority.Normal)
        // {
        //    if (viewToExecuteOn == null)
        //    {
        //        throw new ArgumentNullException(nameof(viewToExecuteOn));
        //    }

        // return viewToExecuteOn.DispatcherQueue.AwaitableRunAsync(function, priority);
        // }

        /// <summary>
        /// Executes the given function asynchronously on given view's UI thread. Default view is the main view.
        /// </summary>
        /// <param name="function">Asynchronous function to be executed asynchronously on UI thread</param>
        /// <param name="priority">Dispatcher execution priority, default is normal</param>
        /// <returns>Awaitable Task/></returns>
        public static Task ExecuteOnUIThreadAsync(Action function, DispatcherQueuePriority priority = DispatcherQueuePriority.Normal)
        {
            // return ExecuteOnUIThreadAsync(CoreApplication.MainView, function, priority);
            return DispatcherQueue.GetForCurrentThread().AwaitableRunAsync(function, priority);
        }

        ///// <summary>
        ///// Executes the given function asynchronously on given view's UI thread. Default view is the main view.
        ///// </summary>
        ///// <typeparam name="T">Returned data type of the function</typeparam>
        ///// <param name="viewToExecuteOn">View for the <paramref name="function"/>  to be executed on </param>
        ///// <param name="function">Synchronous function with return type <typeparamref name="T"/> to be executed on UI thread</param>
        ///// <param name="priority">Dispatcher execution priority, default is normal</param>
        ///// <returns>Awaitable Task with type <typeparamref name="T"/></returns>
        // public static Task<T> ExecuteOnUIThreadAsync<T>(this CoreApplicationView viewToExecuteOn, Func<T> function, DispatcherQueuePriority priority = DispatcherQueuePriority.Normal)
        // {
        //    if (viewToExecuteOn == null)
        //    {
        //        throw new ArgumentNullException(nameof(viewToExecuteOn));
        //    }

        // return viewToExecuteOn.DispatcherQueue.AwaitableRunAsync<T>(function, priority);
        // }

        ///// <summary>
        ///// Executes the given function asynchronously on given view's UI thread. Default view is the main view.
        ///// </summary>
        ///// <typeparam name="T">Returned data type of the function</typeparam>
        ///// <param name="function">Synchronous function to be executed on UI thread</param>
        ///// <param name="priority">Dispatcher execution priority, default is normal</param>
        ///// <returns>Awaitable Task </returns>
        // public static Task<T> ExecuteOnUIThreadAsync<T>(Func<T> function, DispatcherQueuePriority priority = DispatcherQueuePriority.Normal)
        // {
        //    return ExecuteOnUIThreadAsync(CoreApplication.MainView, function, priority);
        // }

        /// <summary>
        /// Extension method for DispatcherQueue. Offering an actual awaitable Task with optional result that will be executed on the given dispatcher
        /// </summary>
        /// <typeparam name="T">Returned data type of the function</typeparam>
        /// <param name="dispatcher">Dispatcher of a thread to run <paramref name="function"/></param>
        /// <param name="function">Asynchrounous function to be executed asynchrounously on the given dispatcher</param>
        /// <param name="priority">Dispatcher execution priority, default is normal</param>
        /// <returns>Awaitable Task with type <typeparamref name="T"/></returns>
        public static Task<T> AwaitableRunAsync<T>(this DispatcherQueue dispatcher, Func<Task<T>> function, DispatcherQueuePriority priority = DispatcherQueuePriority.Normal)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            var taskCompletionSource = new TaskCompletionSource<T>();

            var ignored = dispatcher.TryEnqueue(priority, async () =>
            {
                try
                {
                    var awaitableResult = function();
                    if (awaitableResult != null)
                    {
                        var result = await awaitableResult.ConfigureAwait(false);
                        taskCompletionSource.SetResult(result);
                    }
                    else
                    {
                        taskCompletionSource.SetException(new InvalidOperationException("The Task returned by function cannot be null."));
                    }
                }
                catch (Exception e)
                {
                    taskCompletionSource.SetException(e);
                }
            });

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Extension method for DispatcherQueue. Offering an actual awaitable Task with optional result that will be executed on the given dispatcher
        /// </summary>
        /// <param name="dispatcher">Dispatcher of a thread to run <paramref name="function"/></param>
        /// <param name="function">Asynchrounous function to be executed asynchrounously on the given dispatcher</param>
        /// <param name="priority">Dispatcher execution priority, default is normal</param>
        /// <returns>Awaitable Task</returns>
        public static Task AwaitableRunAsync(this DispatcherQueue dispatcher, Func<Task> function, DispatcherQueuePriority priority = DispatcherQueuePriority.Normal)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            var taskCompletionSource = new TaskCompletionSource<object>();

            var ignored = dispatcher.TryEnqueue(priority, async () =>
            {
                try
                {
                    var awaitableResult = function();
                    if (awaitableResult != null)
                    {
                        await awaitableResult.ConfigureAwait(false);
                        taskCompletionSource.SetResult(null);
                    }
                    else
                    {
                        taskCompletionSource.SetException(new InvalidOperationException("The Task returned by function cannot be null."));
                    }
                }
                catch (Exception e)
                {
                    taskCompletionSource.SetException(e);
                }
            });

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Extension method for DispatcherQueue. Offering an actual awaitable Task with optional result that will be executed on the given dispatcher
        /// </summary>
        /// <typeparam name="T">Returned data type of the function</typeparam>
        /// <param name="dispatcher">Dispatcher of a thread to run <paramref name="function"/></param>
        /// <param name="function"> Function to be executed asynchrounously on the given dispatcher</param>
        /// <param name="priority">Dispatcher execution priority, default is normal</param>
        /// <returns>Awaitable Task</returns>
        public static Task<T> AwaitableRunAsync<T>(this DispatcherQueue dispatcher, Func<T> function, DispatcherQueuePriority priority = DispatcherQueuePriority.Normal)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            var taskCompletionSource = new TaskCompletionSource<T>();

            var ignored = dispatcher.TryEnqueue(priority, () =>
            {
                try
                {
                    taskCompletionSource.SetResult(function());
                }
                catch (Exception e)
                {
                    taskCompletionSource.SetException(e);
                }
            });

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Extension method for DispatcherQueue. Offering an actual awaitable Task with optional result that will be executed on the given dispatcher
        /// </summary>
        /// <param name="dispatcher">Dispatcher of a thread to run <paramref name="function"/></param>
        /// <param name="function"> Function to be executed asynchrounously on the given dispatcher</param>
        /// <param name="priority">Dispatcher execution priority, default is normal</param>
        /// <returns>Awaitable Task</returns>
        public static Task AwaitableRunAsync(this DispatcherQueue dispatcher, Action function, DispatcherQueuePriority priority = DispatcherQueuePriority.Normal)
        {
            return dispatcher.AwaitableRunAsync(
                () =>
                {
                    function();
                    return (object)null;
                }, priority);
        }
    }
}
