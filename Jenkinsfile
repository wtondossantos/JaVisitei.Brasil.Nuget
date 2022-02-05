pipeline {
    agent any
     triggers {
        githubPush()
      }
    stages {
        stage('LS'){
           steps{
               sh 'ls'
            }
         }      
        stage('Restore packages'){
           steps{
               sh 'dotnet restore JaVisitei.Brasil.Nuget.sln'
            }
         }        
        stage('Clean'){
           steps{
               sh 'dotnet clean JaVisitei.Brasil.Nuget.sln --configuration Release'
            }
         }
        stage('Build'){
           steps{
               sh 'dotnet build JaVisitei.Brasil.Nuget.sln --configuration Release --no-restore'
            }
         }
        stage('Test: Unit Test'){
           steps {
                sh 'dotnet test src/JaVisitei.Brasil.Test/JaVisitei.Brasil.Test.csproj --configuration Release --no-restore'
             }
          }
        stage('Publish'){
             steps{
               sh 'dotnet publish src/JaVisitei.Brasil.Nuget/JaVisitei.Brasil.Nuget.csproj --configuration Release --no-restore'
             }
        }
        stage('Deploy'){
             steps{
               sh '''for pid in $(lsof -t -i:3000); do
                       kill -9 $pid
               done'''
               sh 'cd src/JaVisitei.Brasil.Nuget/bin/Release/netcoreapp3.1/publish/'
               sh 'nohup dotnet JaVisitei.Brasil.Nuget.dll --urls="http://3.230.193.223:3000/" --ip="3.230.193.223" --port=3000 --no-restore > /dev/null 2>&1 &'
             }
        }        
    }
}