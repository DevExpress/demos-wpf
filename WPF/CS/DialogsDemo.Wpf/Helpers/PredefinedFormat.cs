namespace DialogsDemo.Helpers {
    public class PredefinedFormat {
        public PredefinedFormat(string example, string format) {
            Example = example;
            Format = format;
        }
        public string Example { get; }
        public string Format { get; }

        public override string ToString() {
            return $"{Example} [{Format}]";
        }
    }
}
