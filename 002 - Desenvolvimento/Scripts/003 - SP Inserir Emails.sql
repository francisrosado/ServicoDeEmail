DROP PROCEDURE spInsereMensagem
Go
CREATE PROCEDURE spInsereMensagem
(
	@DescricaoEmailRementente nVarchar(max),
	@Autenticacao TinYint,
	@Senha nVarchar(max),
	@Smtp nVarchar(max),
	@Porta int,	
	@NomeArquivo nVarchar(max),
	@CaminhoArquivo Varchar(1000),
	@Assunto nVarchar(max),
	@CorpoDaMensagem nVarchar(max),
	@Destinatarios nVarchar(Max)
)
As
begin
	Declare	@Remetente_RemetenteId Int 
	Declare @Anexo_AnexoId Int
	Declare @Item Varchar(800)
	Declare	@DestinariosId Int
	Declare @MensagemId Int
	Declare @executaInsertAnexo Varchar(1000)
	
	Begin Try
		Begin Tran
			If Not Exists ( Select	DescricaoEmail 
							From	Remetentes 
							Where	DescricaoEmail = @DescricaoEmailRementente )
			Begin
    			Insert Into Remetentes ( DescricaoEmail, Autenticacao, Senha, Smtp, Porta )
								values ( @DescricaoEmailRementente, @Autenticacao, @Senha, @Smtp, @Porta )
								
				set @Remetente_RemetenteId = @@IDENTITY
			end
			else
			begin
				set @Remetente_RemetenteId = ( Select	RemetenteId 
											   From		Remetentes 
											   Where	DescricaoEmail = @DescricaoEmailRementente )
			end
			
			If @CaminhoArquivo <> ''
			begin
				Exec(@CaminhoArquivo)
				
				set @Anexo_AnexoId = @@IDENTITY
			end
					
			Insert Into Mensagems  ( Assunto, CorpoDaMensagem, Remetente_RemetenteId, Anexo_AnexoId )
							values ( @Assunto, @CorpoDaMensagem ,@Remetente_RemetenteId ,@Anexo_AnexoId )
							
			set @MensagemId = @@IDENTITY
	    
			Declare CursorItem Cursor For
			select	item 
			from	fnSplit ( @Destinatarios, ',' )

			Open CursorItem

			Fetch Next From CursorItem Into @Item
				While @@FETCH_STATUS = 0
				Begin
					If Not Exists ( Select	DescricaoEmail
									From	Destinarios
									Where	DescricaoEmail = @Item )
					Begin
						Insert Into  Destinarios ( DescricaoEmail )
										Values ( @Item )
										
						Set @DestinariosId = @@IDENTITY 
					end
					Else
					begin
						Set @DestinariosId = ( Select	DestinarioId
											   From		Destinarios
											   Where	DescricaoEmail = @Item )
					End
					
					Insert Into DestinarioMensagems ( Destinario_DestinarioId, Mensagem_MensagemId )
											 Values ( @DestinariosId, @MensagemId  ) 
				
					Fetch Next From CursorItem Into @Item
				End
				close CursorItem
			
		Commit Tran	
		print 'Funcionou!'
	End Try
		
	Begin Catch
		Rollback
		print 'Não funcionou!'
	End Catch	
End    