#Params
param (
    [switch]$dockerize = $false,
    [string]$namespace = "courses-tracking",
    [string]$path = "devops/kubernetes"
)


if ($dockerize) {
    ./devops/pipelines/dockerize.ps1
}
#kubernetes
#Create Namespace
kubectl get ns $namespace 
kubectl create ns $namespace

#Clean
kubectl --namespace=$namespace delete --all deploy
kubectl --namespace=$namespace delete --all services
kubectl --namespace=$namespace delete -f $path/courses-tracking-secrets.yml 
kubectl --namespace=$namespace delete -f $path/courses-tracking-volumes.yml 
kubectl --namespace=$namespace delete -f $path/db/courses-tracking-db-depl.yml 
kubectl --namespace=$namespace delete -f $path/service/courses-tracking-srv-depl.yml 
kubectl --namespace=$namespace delete -f $path/service/courses-tracking-redis-depl.yml 

#Deploy
kubectl --namespace=$namespace apply -f $path/courses-tracking-secrets.yml
kubectl --namespace=$namespace apply -f $path/courses-tracking-volumes.yml
kubectl --namespace=$namespace apply -f $path/db/courses-tracking-db-depl.yml
kubectl --namespace=$namespace apply -f $path/service/courses-tracking-srv-depl.yml
kubectl --namespace=$namespace apply -f $path/service/courses-tracking-redis-depl.yml 
