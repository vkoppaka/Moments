exports.get = function(request, response) {
    var friendshipsTable = request.service.tables.getTable('Friendship');
    if (request.query.getFriends == "true") {    
        var resultsOne = [];
        friendshipsTable.where(function (userId, friendshipAccepted) {
            return this.accepted == friendshipAccepted && this.userId == userId
        }, request.query.userId, true).select('friendId').read ({ 
            success : function (results) 
            {         
                friendshipsTable.where(function (userId, friendshipAccepted) {
                    return this.accepted == friendshipAccepted && this.friendId == userId
                }, request.query.userId, true).select('userId').read({
                    success: function (resultsTwo) { return  getUserFromFriendId (results.concat(resultsTwo));}
                })
        }});
    } else {
        friendshipsTable.where(function (userId, friendshipAccepted) {
            return this.friendId == userId && this.accepted == friendshipAccepted;
        }, request.query.userId, false).select('userId').read({
            success: function (results) { return  getUserFromFriendId (results);}
        });
    }
    
    function getUserFromFriendId (matchingFriendUserIds) {
        var usersTable = request.service.tables.getTable('User');
        console.log ("get user from friend id");
            console.log (matchingFriendUserIds);
            usersTable.where(function(arr) {
                return this.id in arr;
            }, matchingFriendUserIds).read({
                success: function(results) {response.send(statusCodes.OK, results)}
            });
    }
};