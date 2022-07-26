IF NOT EXISTS (SELECT 1
    FROM sys.computed_columns
	WHERE name = N'SurnameSoundex'
		AND object_id = object_id(N'dbo.AccountHolders'))
BEGIN

	PRINT 'Computed column does not exist, so creating it'

	-- If Normal column exists with that name, drop it.
	IF EXISTS(SELECT 1 FROM sys.columns 
          WHERE name = N'SurnameSoundex'
			AND object_id = object_id(N'dbo.AccountHolders'))
	BEGIN

		PRINT 'Column with required name exists, so dropping it'	

		-- Drop it
		ALTER TABLE AccountHolders DROP COLUMN SurnameSoundex

	END

	PRINT 'Creating new computed column'

	-- Now add a new computed column
	ALTER TABLE dbo.AccountHolders ADD SurnameSoundex AS SOUNDEX(Surname) PERSISTED;

END