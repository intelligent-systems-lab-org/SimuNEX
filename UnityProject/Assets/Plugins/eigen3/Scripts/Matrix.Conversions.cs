using System.Text;

public partial class Matrix
{
    /// <summary>
    /// Converts the matrix to a flat array.
    /// </summary>
    /// <param name="rowMajor">If true, the output array will be in row-major order; otherwise, it will be in column-major order.</param>
    /// <returns>A flat array representation of the matrix.</returns>
    public float[] ToArray(bool rowMajor = false)
    {
        float[] data = new float[RowCount * ColCount];
        if (rowMajor)
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColCount; j++)
                {
                    data[(i * ColCount) + j] = this[i, j];
                }
            }
        }
        else
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColCount; j++)
                {
                    data[(j * RowCount) + i] = this[i, j];
                }
            }
        }
        return data;
    }

    /// <summary>
    /// Returns a formatted string of this matrix.
    /// </summary>
    public override string ToString()
    {
        StringBuilder sb = new();
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColCount; j++)
            {
                sb.Append(this[i, j]);
                if (j < ColCount - 1)
                {
                    sb.Append("\t");
                }
            }
            if (i < RowCount - 1)
            {
                sb.Append("\n");
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// Converts the matrix to a 2D array.
    /// </summary>
    /// <returns>A 2D array representation of the matrix.</returns>
    public float[,] To2DArray()
    {
        float[,] data = new float[RowCount, ColCount];
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColCount; j++)
            {
                data[i, j] = this[i, j];
            }
        }
        return data;
    }
}