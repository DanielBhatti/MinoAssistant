namespace MinoAssistant.Board
{
    public class Cell
    {
        private object _visibleValue;
        public object VisibleValue
        {
            get
            {
                if (ActualValueVisibilityOverride) return ActualValue;
                else return _visibleValue;
            }
            set => _visibleValue = value;
        }
        public object ActualValue { get; private set; }
        public bool IsFilled { get => !UnfilledValue.Equals(ActualValue); }
        public object UnfilledValue { get; set; }
        public bool ActualValueVisibilityOverride { get; set; }

        public Cell(object unfilledValue, bool actualValueVisibilityOverride)
        {
            ActualValue = unfilledValue;
            UnfilledValue = unfilledValue;
            VisibleValue = unfilledValue;
            ActualValueVisibilityOverride = actualValueVisibilityOverride;
        }

        public void Fill(object value) => ActualValue = value;

        public void Unfill() => ActualValue = UnfilledValue;
    }
}
