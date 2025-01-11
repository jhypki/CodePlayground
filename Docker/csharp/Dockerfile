FROM mcr.microsoft.com/dotnet/sdk:5.0

WORKDIR /code

RUN dotnet new console -o /code/TemplateProject

RUN rm /code/TemplateProject/Program.cs

WORKDIR /code/TemplateProject
RUN dotnet restore

WORKDIR /code

CMD ["sleep", "infinity"]
