using System;
using System.Collections.Generic;

namespace Admin.Models.Shared
{
    [Serializable]
    public abstract class Message
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }

    [Serializable]
    public sealed class ErrorMessage : Message
    {
        public ErrorMessage()
        {
            Type = Properties.Resources.DefaultMessageTypeError;
            Title = Properties.Resources.DefaultMessageTitleError;
            Text = Properties.Resources.DefaultMessageTextError;
        }

        public ErrorMessage(string message)
        {
            Type = Properties.Resources.DefaultMessageTypeError;
            Title = Properties.Resources.DefaultMessageTitleError;
            Text = message;
        }

        public ErrorMessage(IList<string> message)
        {
            Type = Properties.Resources.DefaultMessageTypeError;
            Title = Properties.Resources.DefaultMessageTitleError;
            Text = string.Join(",", message);
        }

        public ErrorMessage(string message, string title)
        {
            Type = Properties.Resources.DefaultMessageTypeError;
            Title = title;
            Text = message;
        }

        public ErrorMessage(IList<string> message, string title)
        {
            Type = Properties.Resources.DefaultMessageTypeError;
            Title = title;
            Text = string.Join(",", message);
        }
    }

    [Serializable]
    public sealed class SuccessMessage : Message
    {
        public SuccessMessage()
        {
            Type = Properties.Resources.DefaultMessageTypeSuccess;
            Title = Properties.Resources.DefaultMessageTitleSuccess;
            Text = Properties.Resources.DefaultMessageTextSuccess;
        }

        public SuccessMessage(string message)
        {
            Type = Properties.Resources.DefaultMessageTypeSuccess;
            Title = Properties.Resources.DefaultMessageTitleSuccess;
            Text = message;
        }

        public SuccessMessage(IList<string> message)
        {
            Type = Properties.Resources.DefaultMessageTypeSuccess;
            Title = Properties.Resources.DefaultMessageTitleSuccess;
            Text = string.Join(",", message);
        }

        public SuccessMessage(string message, string title)
        {
            Type = Properties.Resources.DefaultMessageTypeSuccess;
            Title = title;
            Text = message;
        }

        public SuccessMessage(IList<string> message, string title)
        {
            Type = Properties.Resources.DefaultMessageTypeSuccess;
            Title = title;
            Text = string.Join(",", message);
        }
    }
}