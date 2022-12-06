using ComplexAlgebra;

namespace Calculus
{
    /// <summary>
    /// A calculator for <see cref="Complex"/> numbers, supporting 2 operations ('+', '-').
    /// The calculator visualizes a single value at a time.
    /// Users may change the currently shown value as many times as they like.
    /// Whenever an operation button is chosen, the calculator memorises the currently shown value and resets it.
    /// Before resetting, it performs any pending operation.
    /// Whenever the final result is requested, all pending operations are performed and the final result is shown.
    /// The calculator also supports resetting.
    /// </summary>
    ///
    /// HINT: model operations as constants
    /// HINT: model the following _public_ properties methods
    /// HINT: - a property/method for the currently shown value
    /// HINT: - a property/method to let the user request the final result
    /// HINT: - a property/method to let the user reset the calculator
    /// HINT: - a property/method to let the user request an operation
    /// HINT: - the usual ToString() method, which is helpful for debugging
    /// HINT: - you may exploit as many _private_ fields/methods/properties as you like
    ///
    /// TODO: implement the calculator class in such a way that the Program below works as expected
    class Calculator
    {
        public const char OperationPlus = '+';
        public const char OperationMinus = '-';
        private Complex _tmpResult;
        private char? _currentOp;
        public Complex Value { set; get; }

        public char? Operation
        {
            set
            {
                // Compute results if there are pending operations.
                if (HasPendingOp)
                {
                    ComputeResult();
                }
                // Updating the old Value to be the temporary result
                // and resetting the current one for the next operation.
                _tmpResult = Value;
                Value = null;
                _currentOp = value;
            }
            get => _currentOp;
        }

        public void ComputeResult()
        {
            switch (_currentOp)
            {
                case OperationPlus:
                    Value = _tmpResult.Plus(Value);
                    break;
                case OperationMinus:
                    Value = _tmpResult.Minus(Value);
                    break;
            }
            _currentOp = null;
        }
        
        public void Reset()
        {
            Value = null;
            _tmpResult = null;
            _currentOp = null;
        }
        
        // The temporary result must be null, if not there are pending operations.
        private bool HasPendingOp => _tmpResult != null;

        public override string ToString() => $"{(Value is null ? "null" : Value.ToString())}, {(Operation is null ? "null" : Operation.ToString())}";

    }
}