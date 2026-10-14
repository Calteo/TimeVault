WITH RankedVersions AS (
    SELECT
        v.*,
        ROW_NUMBER() OVER (
            PARTITION BY v.FileId
            ORDER BY v.LastWrite DESC, v.Number DESC
        ) AS rn
    FROM FileVersions v
)
SELECT
    f.Id AS Id,
    f.Path,
    v.Number,
    v.LastWrite
FROM Files f
LEFT JOIN RankedVersions v
    ON v.FileId = f.Id AND v.rn = 1;