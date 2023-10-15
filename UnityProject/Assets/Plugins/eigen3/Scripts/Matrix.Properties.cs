public partial class Matrix
{
    /// <summary>
    /// Get number of rows.
    /// </summary>
    public int RowCount => Eigen3.GetRowCount(_matrixPtr);
    /// <summary>
    /// Get number of columns.
    /// </summary>
    public int ColCount => Eigen3.GetColumnCount(_matrixPtr);

    /// <summary>
    /// Gets or sets the matrix element at the specified row and column.
    /// </summary>
    /// <param name="row">The zero-based row index of the element to get or set.</param>
    /// <param name="col">The zero-based column index of the element to get or set.</param>
    /// <returns>The matrix element at the specified row and column.</returns>
    public float this[int row, int col]
    {
        get => Eigen3.GetElement(_matrixPtr, row, col);
        set => Eigen3.SetElement(_matrixPtr, row, col, value);
    }
}
