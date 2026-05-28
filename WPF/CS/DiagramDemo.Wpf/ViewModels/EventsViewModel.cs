using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using DevExpress.Data.Utils;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    [POCOViewModel]
    public class EventsViewModel {
        protected EventsViewModel() { }
        readonly ObservableCollection<LogEntry> log = new ObservableCollection<LogEntry>();
        const int LogEntriesMaxCount = 100;
        public IEnumerable<LogEntry> Log { get { return log; } }

        void AddToLog(string eventName, string argsFormat, params object[] args) {
            log.Add(LogEntry.Create(eventName, string.Format(argsFormat, args)));
            if(log.Count > LogEntriesMaxCount)
                log.RemoveAt(0);
            var scrollService = this.GetService<IScrollToEndService>();
            if(scrollService != null)
                scrollService.ScrollToEnd();
        }
        public void ClearLog() {
            log.Clear();
        }
        public virtual IEnumerable<DiagramEventNode> EventsInfo { get; protected set; }
        public void InitializeEventsInfo(DiagramControl eventsOwner) {
            EventsInfo = new DiagramEventsInfo(eventsOwner, AddToLog).Initialize();
        }
    }
    [POCOViewModel]
    public class LogEntry {
        public static LogEntry Create(string eventName, string eventArgs) {
            return ViewModelSource.Create(() => new LogEntry(eventName, eventArgs));
        }
        protected LogEntry(string eventName, string eventArgs) {
            EventName = eventName;
            EventArgs = eventArgs;
        }
        public virtual string EventName { get; protected set; }
        public virtual string EventArgs { get; protected set; }
        public void ShowHelp() {
            var fileName = $"https://documentation.devexpress.com/WPF/DevExpress.Xpf.Diagram.DiagramControl.{EventName}.event";
            SafeProcess.Start(fileName, null, startInfo => { startInfo.UseShellExecute = true; });
        }
    }
}
