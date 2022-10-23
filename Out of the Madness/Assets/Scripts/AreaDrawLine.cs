using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AreaDrawLine
{
    public static void DrawRectange(Vector2 top, Vector2 bottom)
    {
        Vector2 center_offset = (top + bottom) * 0.5f;
        Vector2 displacement_vector = top - bottom;
        float x_projection = Vector2.Dot(displacement_vector, Vector2.right);
        float y_projection = Vector2.Dot(displacement_vector, Vector2.up);

        Vector2 TopLeftCorner = new Vector2(-x_projection * 0.5f, y_projection * 0.5f) + center_offset;
        Vector2 BottomRightCorner = new Vector2(x_projection * 0.5f, -y_projection * 0.5f) + center_offset;

        Gizmos.DrawLine(top, TopLeftCorner);
        Gizmos.DrawLine(TopLeftCorner, bottom);
        Gizmos.DrawLine(bottom, BottomRightCorner);
        Gizmos.DrawLine(BottomRightCorner, top);
    }
}
