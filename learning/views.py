from rest_framework import viewsets
from learning.models import Test, LearningResults
from learning.services.serializers import TestSerializer, TestResultsSerializer
from rest_framework.permissions import AllowAny


class TestViewSet(viewsets.ReadOnlyModelViewSet):
    queryset = Test.objects.all()
    serializer_class = TestSerializer
    permission_classes = [AllowAny]


class TestResultViewset(viewsets.ReadOnlyModelViewSet):
    queryset = LearningResults.objects.all()
    serializer_class = TestResultsSerializer
    permission_classes = [AllowAny]
