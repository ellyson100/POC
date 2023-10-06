# POC

# Como rodar
Executar o comando no terminal dentro da pasta POC/POC.API para criar a imagem. &nbsp;
docker build . -t poc
&nbsp;
Após o build com sucesso execute o comando. &nbsp;
docker run -p 80:80 poc —name poc
&nbsp;
Para acessar a apliação utilizei a url: &nbsp;
http://localhost/swagger/index.html
