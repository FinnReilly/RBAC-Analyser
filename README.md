# RBAC-Analyser
RBAC Analyser app for AD Access Analysis
## Analyse and compare AD users
RBAC Analyser allows you to pull user access information from your AD domain and manipulate and query it 
in ways which are not possible in the standard AD Users and Computers MMC snap-in:

* Use the Include and Exclude fields in the config tab to restrict the user dataset being analysed
* Group users by Description or Title, and view the relative presence of AD groups within each grouping
* Output text-file or CSV reports of users/groupings access
* Output XML reports of a given dataset, which can be opened in the application at a later time, or by users without AD access
* Import additional permissions not listed in AD from CSV
* Use the K-nearest neighbours functionality to see which users or groupings are most similar to a given user or grouping
* Recommend a user template based on a selection of users or groupings
* Cluster users and groupings with a choice of algorithms, recommend templates per cluster
* Visualise relationships between different users/titles/descriptions access with the Map functionality

My aim in writing this software was to make it easier to understand and compare users' access, with the goal
of aiding businesses in complying with the Role Based Access Control provisions of the ISO27001 security standard,
using Data Science/Machine Learning techniques.  I have personally used it to detect anomalies and produce reports
in the workplace.

## Files Included

* All code files
* A compiled exe of the latest version (this is bundled with its dependencies using ILMerge)
