Use ServicoDeEmail
GO
Insert Into Remetentes (	DescricaoEmail, Autenticacao, Senha, SMTP, Porta  )  values ( 'francis@csamaritima.com', 2,'1234','mail.csamaritima.com',587 );	
Insert Into Remetentes (	DescricaoEmail, Autenticacao, Senha, SMTP, Porta  )  values ( 'eta@csamaritima.com', 2,'csam15','mail.csamaritima.com',25 );	
GO
Insert into	Destinarios ( DescricaoEmail )	values	( 'ffrancisr@gmail.com');
Insert into	Destinarios ( DescricaoEmail )	values	( 'francis.rosado@yahoo.com.br' );
insert into Destinarios ( DescricaoEmail )  values  ( 'maritime@csamaritima.com' );
Go
 INSERT INTO Anexoes VALUES ('Arquivo Anexo',(SELECT * FROM OPENROWSET(BULK 'F:\Estudos\DIVERSOS\AutoConfianca.Pdf', SINGLE_BLOB) AS A))
Go
Insert Into Mensagems ( Assunto, CorpoDaMensagem, Remetente_RemetenteId, Anexo_AnexoId ) Values ( 'Email de Teste','Teste sobre o novo servidor de email',1 ,1 );
Insert Into Mensagems ( Assunto, CorpoDaMensagem, Remetente_RemetenteId, Anexo_AnexoId ) Values ( 'Email de Teste 2','Teste sobre o novo servidor de email',1 ,1  );
Insert Into Mensagems ( Assunto, CorpoDaMensagem, Remetente_RemetenteId, Anexo_AnexoId ) Values ( 'Email de Teste 3','Teste sobre o novo servidor de email',1 ,1  );
Insert Into Mensagems ( Assunto, CorpoDaMensagem, Remetente_RemetenteId, Anexo_AnexoId ) Values ( 'Email de Teste 4','Teste sobre o novo servidor de email',1 ,1  );
Insert Into Mensagems ( Assunto, CorpoDaMensagem, Remetente_RemetenteId, Anexo_AnexoId ) Values ( 'Email ETA para MARITIME','Este Email devera ser enviado de Eta para Maritime.',2,1 );
GO
Insert Into DestinarioMensagems ( Destinario_DestinarioID, Mensagem_MensagemId ) values (1,1)
Insert Into DestinarioMensagems ( Destinario_DestinarioID, Mensagem_MensagemId ) values (1,2)
Insert Into DestinarioMensagems ( Destinario_DestinarioID, Mensagem_MensagemId ) values (1,3)
Insert Into DestinarioMensagems ( Destinario_DestinarioID, Mensagem_MensagemId ) values (1,4)

Insert Into DestinarioMensagems ( Destinario_DestinarioID, Mensagem_MensagemId ) values (2,1)
Insert Into DestinarioMensagems ( Destinario_DestinarioID, Mensagem_MensagemId ) values (2,2)
Insert Into DestinarioMensagems ( Destinario_DestinarioID, Mensagem_MensagemId ) values (2,3)
Insert Into DestinarioMensagems ( Destinario_DestinarioID, Mensagem_MensagemId ) values (2,4)

Insert Into DestinarioMensagems ( Destinario_DestinarioID, Mensagem_MensagemId ) values (3,5)
GO

Select	* From	DestinarioMensagems
Select  * From	Mensagems 
Select * From	Destinarios
Select * From	Remetentes
Select * From	Anexoes



