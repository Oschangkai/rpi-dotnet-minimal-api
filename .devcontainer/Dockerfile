FROM arm64v8/debian:bullseye

# https://learn.microsoft.com/en-us/dotnet/core/install/linux-debian#dependencies
RUN apt update \
  && apt install --no-install-recommends -y wget ca-certificates libc6 libicu67 git \
  && rm -rf /var/lib/apt/lists/*

# https://learn.microsoft.com/en-us/dotnet/core/install/linux-debian#unable-to-find-package
# https://learn.microsoft.com/en-us/dotnet/core/install/linux-scripted-manual#scripted-install
RUN wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh \
  && chmod +x dotnet-install.sh \
  && ./dotnet-install.sh -c 8.0 \
  && rm dotnet-install.sh
ENV DOTNET_ROOT=/root/.dotnet
ENV PATH=$PATH:$DOTNET_ROOT:$DOTNET_ROOT/tools

# https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/?tabs=net8plus,linux-ubuntu#prerequisites
RUN apt update \
  && apt install --no-install-recommends -y clang zlib1g-dev \
  && rm -rf /var/lib/apt/lists/*