dotnet clean
dotnet build
dotnet publish -c Release -o release
docker build -t egonzalezatos/trainee-tracking . -f ./devops/Docker/Dockerfile
docker push egonzalezatos/trainee-tracking 
kubectl delete -f ./devops/kubernetes/Service/courses-tracking-srv-depl.yml
kubectl apply  -f ./devops/kubernetes/Service/courses-tracking-srv-depl.yml