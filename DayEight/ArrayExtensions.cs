
// todo - alternative method - is tree surrounded by all taller trees? if so, ignore

public class ArrayExtensions<T>
{
    public static int[][] Transpose(int[][] matrix)
    {
        var rows = matrix[0].GetLength(0);
        var columns = matrix.GetLength(0);

        var result = new int[columns][];
        for (int i = 0; i < columns; i++)
        {
            result[i] = new int[rows];
        }

        for (var c = 0; c < columns; c++)
        {
            for (var r = 0; r < rows; r++)
            {
                result[c][r] = matrix[r][c];
            }
        }
        return result;
    }
}
