We can take advantage of the commands provided by `sfdx` to create a series of scripts that automate our development lifecycle. 

```bash
while getopts m:b:d:n: option
do
case "${option}"
in
m) MESSAGE=${OPTARG};;
b) BRANCH=${OPTARG};;
d) DEFAULTORG=${OPTARG};;
n) SCRATCHORGNAME=${OPTARG};;
esac
done
git add .
git commit -m "${MESSAGE}"
git push -u origin ${BRANCH}
sfdx force:org:create -v ${DEFAULTORG} -f /config/project-scratch-def.json -a ${SCRATCHORGNAME} --wait 3
sfdx force:org:display -u ${SCRATCHORGNAME}
sfdx force:source:push -u ${SCRATCHORGNAME}
```

- `while getopts m:b:d:n: option` is saying that we want to loop through our parameters that are passed into the script. 
- `m:b:d:n:` is saying that each of these parameters should be considered a *required* parameter. The *required* attribute is made known by using the `:` after the given parameter. 
- `m) MESSAGE=${OPTARG};;` says to create a variable called `MESSAGE` and set it to whatever value was provided by the `m` parameter passed into the script. 
- `git add .` stages all new files and folders within our local repository
- `git commit -m "${MESSAGE}"` commits all staged changes to our local repository using a commit message that was passed as a parameter
- `git push -u origin ${BRANCH}` pushes our local branch up to our remote branch, as specified by the `-b` parameter
- `sfdx force:org:create -v ${DEFAULTORG} -f /config/project-scratch-def.json -a ${SCRATCHORGNAME} --wait 3` uses the `sfdx` CLI to create a scratch org based off of our scratch definition file, with a given name as defined by `SCRATCHORGNAME`, and based off of the Dev Hub org provided by the `DEFAULTORG` parameter.
- `sfdx force:org:display -u ${SCRATCHORGNAME}` prints out the details of our scratch org.
- `sfdx force:source:push -u ${SCRATCHORGNAME}` pushes our new source code to the scratch org

We can execute this script via the following command:
```bash
./sample-script.sh -m "Added some cool new feature" -b task-featureA -d DevHub -n testScratchOrg
```