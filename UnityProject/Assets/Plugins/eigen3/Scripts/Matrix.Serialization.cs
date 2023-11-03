using UnityEngine;

/// <summary>
/// Serialization responsibilities of the <see cref="Matrix"/> class
/// </summary>
public partial class Matrix : ISerializationCallbackReceiver
{
    [SerializeField]
    private float[] _serializedData;

    [SerializeField]
    private int _serializedRows;

    [SerializeField]
    private int _serializedCols;

    public void OnBeforeSerialize()
    {
        // Convert matrix to a flat array and store it in _serializedData.
        _serializedData = ToArray(rowMajor: true);
        _serializedRows = RowCount;
        _serializedCols = ColCount;
    }

    public void OnAfterDeserialize()
    {
        // Check if there is serialized data.
        if (_serializedData != null && _serializedRows > 0 && _serializedCols > 0)
        {
            // Reconstruct the Eigen3 matrix from the serialized data.
            _matrixPtr = Eigen3.CreateMatrix(_serializedRows, _serializedCols, _serializedData, rowMajor: true);
        }
    }
}
