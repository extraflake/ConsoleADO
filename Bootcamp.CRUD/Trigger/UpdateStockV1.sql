CREATE TRIGGER [TestTrigger]
	ON [dbo].[TransactionsItems]
	FOR INSERT
	AS
	DECLARE @Item_Id int;
	DECLARE @Quantity int;
	DECLARE @Transact_Id int;
	DECLARE @Stock int;
	DECLARE @PROCEED int;
	BEGIN
		SELECT 
		 @Item_Id = ti.Items_Id, 
		 @Quantity = ti.Quantity, 
		 @Transact_Id = ti.Transaction_Id 
		FROM 
		 inserted ti;
		 
		SELECT 
		 @Stock = Items.Stock
		FROM
		 Items
		WHERE
		 Id = @Item_Id;
		 
		UPDATE
		 Items
		SET
		 Stock = @Stock - @Quantity
		Where
		 Id = @Item_Id;
	END
