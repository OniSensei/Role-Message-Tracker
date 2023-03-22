Imports Discord
Imports Discord.WebSocket

Module Program
    Sub Main(args As String())
        MainAsync.GetAwaiter.GetResult()
    End Sub

    Public clientconfig As DiscordSocketConfig = New DiscordSocketConfig With {
        .TotalShards = 1,
        .GatewayIntents = GatewayIntents.All
    }
    Public _client As DiscordShardedClient = New DiscordShardedClient(clientconfig)

    Sub New()
        ' Set console encoding for names with symbols like ♂️ and ♀️
        Console.OutputEncoding = Text.Encoding.UTF8
        ' Set our log, ready, timer, and message received functions
        AddHandler _client.Log, AddressOf LogAsync
        AddHandler _client.MessageReceived, AddressOf MessageReceivedAsync
        AddHandler _client.ShardConnected, AddressOf ShardConnectedAsync
        AddHandler _client.ShardReady, AddressOf ShardReadyAsync
    End Sub

    <STAThread()>
    Public Async Function MainAsync() As Task
        ' Set thread
        ' Threading.Thread.CurrentThread.SetApartmentState(Threading.ApartmentState.STA)

        Dim token As String = GetBotToken() ' Gets token from settings file
        Await _client.LoginAsync(TokenType.Bot, token)

        ' Wait for the client to start
        Await _client.StartAsync
        Await Task.Delay(-1)
    End Function

    Private Async Function LogAsync(ByVal log As LogMessage) As Task(Of Task)
        ' Once loginasync and startasync finish we get the log message of "Ready" once we get that, we load everything else
        If log.ToString.Contains("Ready") Then
            Colorize("[GATEWAY]   " & log.ToString)
        ElseIf log.ToString.Contains("gateway") Or log.ToString.Contains("unhandled") Then
        Else
            Colorize("[GATEWAY]   " & log.ToString) ' update console
        End If
        Return Task.CompletedTask
    End Function

    Private Async Function ShardConnectedAsync(ByVal shard As DiscordSocketClient) As Task(Of Task)
        Colorize("[SHARD]     #" & shard.ShardId + 1 & " connected! Guilds: " & shard.Guilds.Count & " Users: " & shard.Guilds.Sum(Function(x) x.MemberCount))
        Return Task.CompletedTask
    End Function

    Private Async Function ShardReadyAsync(ByVal shard As DiscordSocketClient) As Task(Of Task)
        Colorize("[SHARD]     #" & shard.ShardId + 1 & " ready! Guilds: " & shard.Guilds.Count & " Users: " & shard.Guilds.Sum(Function(x) x.MemberCount))
        Return Task.CompletedTask
    End Function

    Private Async Function MessageReceivedAsync(ByVal message As SocketMessage) As Task
        Dim author As SocketGuildUser = message.Author ' Assigns the socket guild user to a variable
        Dim channel As SocketGuildChannel = message.Channel
        Dim guild As SocketGuild = channel.Guild

        If author.IsBot = False Then ' Checks if the message is from a user
            Dim prefix As String = GetBotPrefix() ' Gets prefix from settings file

            If Not message.Content.StartsWith(prefix) Then ' Checks if the message is NOT a command
                Try
                    Colorize("[INFO]     Message sent from " & author.Username)
                    Dim roles As String = GetRolesList() ' Get roles from settings
                    Dim roleChannels As String = GetRolesChannelList() ' Get channels for output from settings
                    Dim rolesList As New List(Of String)
                    Dim rolesChannelList As New List(Of String)
                    If roles.Contains(",") Then ' Checks if there is more than 1 role
                        Dim roleSplit As String() = roles.Split(",") ' Splits the roles in the settings
                        Dim roleChannelSplit As String() = roleChannels.Split(",") ' Splits the channels in the settings
                        For r As Integer = 0 To roleSplit.Count - 1
                            rolesList.Add(roleSplit(r)) ' Adds each role to the list
                            rolesChannelList.Add(roleChannelSplit(r)) ' Adds each channel to the list
                        Next
                    Else
                        rolesList.Add(roles) ' 1 role gets added to the list
                        rolesChannelList.Add(roleChannels) ' 1 channel gets added to the list
                    End If
                    Colorize("[INFO]     Checking for roles")

                    For i As Integer = 0 To rolesList.Count - 1 ' Loop through the roles listed
                        Dim roleName As String = rolesList(i) ' Assign the name to variable since lambda expressions prefer this method
                        If author.Roles.Any(Function(r) r.Name = roleName) Then ' Check if the user has the role
                            ' Role found
                            Colorize("[INFO]     Role found: " & roleName)

                            Dim channelID As String = rolesChannelList(i)
                            Dim builder As EmbedBuilder = New EmbedBuilder
                            builder.WithAuthor(author.Username, GetBotIcon)
                            builder.WithThumbnailUrl(author.GetAvatarUrl)
                            builder.WithColor(219, 172, 69)

                            builder.WithDescription(message.Content) ' Put message content into embed description
                            If message.Attachments.Count > 0 Then
                                builder.WithImageUrl(message.Attachments(0).Url) ' If there is an image, include it.
                            End If
                            builder.AddField("Original Message:", "[View](" & message.GetJumpUrl & ")") ' Link to original message

                            Await _client.GetGuild(guild.Id).GetTextChannel(channelID).SendMessageAsync("", False, builder.Build)
                            Colorize("[INFO]     Message posted to <#" & channelID & ">")
                        End If
                    Next
                Catch ex As Exception
                    Colorize("[ERROR]    " & ex.ToString)
                End Try
            End If
        End If
    End Function

    Public Sub Colorize(ByVal msg As String)
        ' Checks the message for particular string and changes the color, then updates the log
        Select Case True
            Case msg.Contains("ERROR")
                Console.ForegroundColor = ConsoleColor.DarkRed
                Console.WriteLine(msg)
                Console.ResetColor()
            Case msg.Contains("INFO")
                Console.ForegroundColor = ConsoleColor.DarkYellow
                Console.WriteLine(msg)
                Console.ResetColor()
            Case msg.Contains("GATEWAY")
                Console.ForegroundColor = ConsoleColor.DarkMagenta
                Console.WriteLine(msg)
                Console.ResetColor()
            Case Else
                Console.ForegroundColor = ConsoleColor.White
                Console.WriteLine(msg)
                Console.ResetColor()
        End Select
    End Sub
End Module
