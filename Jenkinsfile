pipeline {
    agent any
     triggers {
        githubPush()
      }
    stages {
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
               sh '''for pid in $(lsof -t -i:9090); do
                       kill -9 $pid
               done'''
               sh 'cd src/JaVisitei.Brasil.Nuget/bin/Release/netcoreapp3.1/publish/'
               sh 'nohup dotnet JaVisitei.Brasil.Nuget.dll --urls="http://104.128.91.189:9090" --ip="104.128.91.189" --port=9090 --no-restore > /dev/null 2>&1 &'
             }
        }        
    }
}