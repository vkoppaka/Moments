exports.get = function (request, response) {
    var azure = require('azure');
    var qs = require('querystring');
    var appSettings = require('mobileservice-config').appSettings;

    var accountName = appSettings.STORAGE_ACCOUNT_NAME;
    var accountKey = appSettings.STORAGE_ACCOUNT_ACCESS_KEY;
    var host = accountName + '.blob.core.windows.net';
    
        // Set the BLOB store container name on the item, which must be lowercase.
        request.query.containerName = request.query.containerName.toLowerCase();

        // If it does not already exist, create the container 
        // with public read access for blobs.        
        var blobService = azure.createBlobService(accountName, accountKey, host);
        blobService.createContainerIfNotExists (request.query.containerName, {
            publicAccessLevel: 'blob'
        }, function (error) {
                // Provide write access to the container for the next 5 mins.        
                var sharedAccessPolicy = {
                    AccessPolicy: {
                        Permissions: azure.Constants.BlobConstants.SharedAccessPermissions.WRITE,
                        Expiry: new Date(new Date().getTime() + 5 * 60 * 1000)
                    }
                };

                // Generate the upload URL with SAS for the new image.
                var sasQueryUrl = blobService.generateSharedAccessSignature(request.query.containerName, '', sharedAccessPolicy);

                // Set the query string.
                var sasQueryString = qs.stringify(sasQueryUrl.queryString);

                // Set the full path on the new new item, 
                // which is used for data binding on the client. 
                var imageUri = sasQueryUrl.baseUrl + sasQueryUrl.path; 
                // response.send (statusCodes.OK, JSON.stringify(imageUri));
                response.send (statusCodes.OK, JSON.stringify(sasQueryString));
        });
};